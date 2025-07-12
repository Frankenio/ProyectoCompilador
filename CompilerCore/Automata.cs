namespace CompilerCore
{
    public class Automata
    {
        public static bool EsId(string source)
        {
            if (string.IsNullOrEmpty(source)) return false;

            StatusEnum state = StatusEnum.q0;

            foreach (char c in source)
            {
                switch (state)
                {
                    case StatusEnum.q0:
                        if (c == '_')
                            state = StatusEnum.q1;
                        else if (char.IsLetter(c))
                            state = StatusEnum.q2;
                        else
                            return false;
                        break;

                    case StatusEnum.q1:
                        if (char.IsLetter(c))
                            state = StatusEnum.q2;
                        else
                            return false;
                        break;

                    case StatusEnum.q2:
                        if (char.IsLetterOrDigit(c) || c == '_')
                            state = StatusEnum.q3;
                        else
                            return false;
                        break;

                    case StatusEnum.q3:
                        if (char.IsLetterOrDigit(c) || c == '_')
                            state = StatusEnum.q3;
                        else
                            return false;
                        break;
                }
            }

            // Aceptamos si termina en q2 o q3 (porque ya se pasó por la letra obligatoria)
            return state == StatusEnum.q2 || state == StatusEnum.q3;
        }

        public static bool IsComentarioLinea(string source)
        {
            if (string.IsNullOrEmpty(source)) return false;

            StatusEnum state = StatusEnum.q0;

            foreach (char c in source)
            {
                switch (state)
                {
                    case StatusEnum.q0:
                        if (c == '/')
                            state = StatusEnum.q1;
                        else
                            return false;
                        break;

                    case StatusEnum.q1:
                        if (c == '/')
                            state = StatusEnum.q2;
                        else
                            return false;
                        break;

                    case StatusEnum.q2:
                        // Permite cualquier carácter en q2
                        state = StatusEnum.q2;
                        break;
                }
            }

            return state == StatusEnum.q2;
        }

        public static bool IsEntero(string source)
        {
            if (string.IsNullOrEmpty(source)) return false;

            StatusEnum state = StatusEnum.q0;

            foreach (char c in source)
            {
                switch (state)
                {
                    case StatusEnum.q0:
                        if (c == '+' || c == '-')
                            state = StatusEnum.q1;
                        else if (char.IsDigit(c))
                            state = StatusEnum.q2;
                        else
                            return false;
                        break;

                    case StatusEnum.q1:
                        if (char.IsDigit(c))
                            state = StatusEnum.q2;
                        else
                            return false;
                        break;

                    case StatusEnum.q2:
                        if (char.IsDigit(c))
                            state = StatusEnum.q3;
                        else
                            return false;
                        break;

                    case StatusEnum.q3:
                        if (char.IsDigit(c))
                            state = StatusEnum.q3;
                        else
                            return false;
                        break;
                }
            }

            // Aceptamos si termina en q2 o q3 (al menos un dígito leído)
            return state == StatusEnum.q2 || state == StatusEnum.q3;
        }

        public static bool IsReal(string source)
        {
            if (string.IsNullOrEmpty(source)) return false;

            StatusEnum state = StatusEnum.q0;

            foreach (char c in source)
            {
                switch (state)
                {
                    case StatusEnum.q0:
                        if (c == '+' || c == '-')
                            state = StatusEnum.q1;
                        else if (char.IsDigit(c))
                            state = StatusEnum.q2;
                        else
                            return false;
                        break;

                    case StatusEnum.q1:
                        if (char.IsDigit(c))
                            state = StatusEnum.q2;
                        else
                            return false;
                        break;

                    case StatusEnum.q2:
                        if (char.IsDigit(c))
                            state = StatusEnum.q2;
                        else if (c == '.')
                            state = StatusEnum.q3;
                        else
                            return false;
                        break;

                    case StatusEnum.q3:
                        if (char.IsDigit(c))
                            state = StatusEnum.q4;
                        else
                            return false;
                        break;

                    case StatusEnum.q4:
                        if (char.IsDigit(c))
                            state = StatusEnum.q4;
                        else
                            return false;
                        break;
                }
            }

            // Se acepta solo si termina en q4 (número completo con punto y decimales)
            return state == StatusEnum.q4;
        }
    }
}