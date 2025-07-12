namespace CompilerCore
{
    public class Sintactico
    {
        public List<string> _errors { get; private set; }
        public bool Analizar(List<Simbolo> ts)
        {
            _errors = new(); int idx = 0;
            if(!Clase(ts, ref idx) || idx<ts.Count) return false;
            return true;
        }

        private bool Clase(List<Simbolo> ts, ref int idx)
        {
            // [Modificador]* class Id { Campos }
            if(!Match(ts, ref idx, "Modificador")){_errors.Add($"Se esperaba tipo un tipo de modificador en la linea {ts[idx].Linea} y columna {ts[idx].Columna}."); return false;}
            if(!Match(ts, ref idx, "Clase")) {_errors.Add($"Se esperaba el lexema class la linea {ts[idx].Linea} y columna {ts[idx].Columna}.");return false;}
            if(!Match(ts, ref idx, "Id")) {_errors.Add($"Se esperaba identificador de clase en la linea {ts[idx].Linea} y columna {ts[idx].Columna}."); return false;}
            if(!Match(ts, ref idx, "LlaveAbre")) {_errors.Add("Se esperaba '{' en la linea {ts[idx].Linea} y columna {ts[idx].Columna}."); return false;}
            while(idx<ts.Count && ts[idx].Token!="LlaveCierra")
                if(!Campo(ts, ref idx)) return false;
            if(!Match(ts, ref idx, "LlaveCierra")) {_errors.Add("Se esperaba '} en la linea {ts[idx].Linea} y columna {ts[idx].Columna}.'"); return false;}
            return true;
        }

        private bool Campo(List<Simbolo> ts, ref int idx)
        {
            while (idx < ts.Count && ts[idx].Token == "Atributo")
                {
                    idx++; // Ignora o almacena si quieres usarlos después
                }
            // Tipo Id ;
            if(idx<ts.Count && ts[idx++].Token!="Modificador") {_errors.Add($"Se esperaba tipo un tipo de modificador en la linea {ts[idx].Linea} y columna {ts[idx].Columna}."); return false;}
            if(!Match(ts, ref idx, "Tipo")) {_errors.Add($"Se esperaba tipo un tipo de dato en la linea {ts[idx].Linea} y columna {ts[idx].Columna}."); return false;}
            if(!Match(ts, ref idx, "Id")) {_errors.Add($"Se esperaba identificador en la linea {ts[idx].Linea} y columna {ts[idx].Columna}."); return false;}
            if(!Match(ts, ref idx, "Fin")) {_errors.Add($"Se esperaba ';' en la linea {ts[idx].Linea} y columna {ts[idx].Columna}."); return false;}
            return true;
        }

        private bool Match(List<Simbolo> ts, ref int idx, string token)
        {
            if(idx<ts.Count && ts[idx].Token==token) {idx++; return true;}
            return false;
        }   
    }
}