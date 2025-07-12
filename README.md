# ProyectoCompilador

Este proyecto consiste en la implementación de un compilador básico con interfaz gráfica, desarrollado en C# usando Windows Forms (.NET 9). El sistema incluye análisis léxico, sintáctico y traducción SQL desde un lenguaje fuente simulado.

## 🧩 Estructura del proyecto

- **CompilerCore/**: Contiene el núcleo del compilador, incluyendo el analizador léxico (`Lexico.cs`), sintáctico (`Sintactico.cs`), la tabla de símbolos, y la traducción a SQL.
- **CompilerUIWinForms/**: Proyecto de interfaz gráfica usando WinForms para interactuar con el compilador.
- [**Instalador de Prueba**](blob:https://github.com/c48ffa97-9ec9-4163-8742-97ef0fef5ca2): Este es solo requiere Windows 8.1+.
- **Archivos auxiliares**: algunos archivos  `.java` se usan como recursos de prueba

## 🚀 Características principales

- Análisis léxico de cadenas fuente.
- Análisis sintáctico basado en gramáticas definidas.
- Traducción intermedia a instrucciones SQL.
- Interfaz de usuario gráfica amigable para probar entradas.

## 🛠️ Tecnologías

- Lenguaje: C# (.NET 9)
- Interfaz: WinForms
- IDE sugerido: Visual Studio Code (VS Code)
- Plataforma: Windows (8.1+)

## 💡 Uso

1. Clona el repositorio:
   ```bash
   git clone https://github.com/Frankenio/ProyectoCompilador.git
