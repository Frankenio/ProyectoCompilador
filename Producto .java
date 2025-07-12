public class Producto {
    private long id;            // id INTEGER PRIMARY KEY AUTOINCREMENT
    private String nombre;      // nombre TEXT NOT NULL
    private String descripcion; // descripcion TEXT
    private double precio;      // precio REAL NOT NULL (usamos double para el precio)
    private int stock;          // stock INTEGER NOT NULL (cantidad en inventario)
}