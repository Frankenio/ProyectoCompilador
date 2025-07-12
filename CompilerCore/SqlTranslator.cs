using System.Text;
using System.Text.RegularExpressions;


namespace CompilerCore
{
    public static class SqlTranslator
    {
        /// <summary>Genera un CREATE TABLE SQL a partir de los campos de la clase.</summary>
        public static string TranslateClass(List<Simbolo> ts)
        {
            // 1. Obtener nombre de la clase con un for tradicional
            string className = null;
            for (int j = 0; j < ts.Count - 1; j++)
            {
                if (ts[j].Token == "Clase" && ts[j+1].Token == "Id")
                {
                    className = ts[j+1].Lexema;
                    break;
                }
            }
            if (className == null)
                throw new Exception("No se encontró nombre de clase");

            // 2. Preparar listas
            var columnDefs  = new List<string>();
            var foreignKeys = new List<string>();
            var pending     = new List<string>();

            // 3. Recorrer tokens para columnas y atributos
            for (int i = 0; i < ts.Count; i++)
            {
                var s = ts[i];
                if (s.Token == "Atributo")
                {
                    pending.Add(s.Lexema.Trim('[', ']').Trim().ToLower());
                    continue;
                }

                if (s.Token == "Tipo" && i + 1 < ts.Count && ts[i+1].Token == "Id")
                {
                    string tipo   = s.Lexema;
                    string nombre = ts[i+1].Lexema;
                    string sqlTipo= MapToSQLite(tipo);
                    var extras    = new List<string>();

                    foreach (var attr in pending)
                    {
                        if (attr == "key")      extras.Add("PRIMARY KEY AUTOINCREMENT");
                        if (attr == "not null") extras.Add("NOT NULL");
                        if (attr == "unique")   extras.Add("UNIQUE");

                        if (attr.StartsWith("foreign"))
                        {
                            var m = Regex.Match(attr,
                            @"foreign\s+([A-Za-z_][A-Za-z0-9_]*)\s*\(\s*([A-Za-z_][A-Za-z0-9_]*)\s*\)");
                            if (m.Success)
                            {
                                string refTable  = m.Groups[1].Value;
                                string refColumn = m.Groups[2].Value;
                                foreignKeys.Add($"FOREIGN KEY ({nombre}) REFERENCES {refTable}s({refColumn})");
                            }
                        }
                    }

                    // Definición de la columna
                    var line = $"  {nombre} {sqlTipo}";
                    if (extras.Count > 0)
                        line += " " + string.Join(" ", extras);
                    columnDefs.Add(line);

                    pending.Clear();
                }
            }

            // 4. Armar el CREATE TABLE
            var allLines = columnDefs
                .Concat(foreignKeys.Select(fk => "  " + fk))
                .ToList();

            var sb = new StringBuilder();
            sb.AppendLine($"CREATE TABLE {className.ToLower()}s (");
            sb.AppendLine(string.Join(",\n", allLines));
            sb.AppendLine(");");

            return sb.ToString();
        }
        private static string MapToSQLite(string tipo)
        {
            return tipo switch
            {
                "int"          => "INTEGER",
                "long"         => "INTEGER",
                "float"        => "REAL",
                "double"       => "REAL",
                "boolean"      => "INTEGER",
                "char"         => "TEXT",
                "string"       => "TEXT",
                _              => "TEXT",
            };
        }
    }
}