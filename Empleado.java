public class Empleado
{
    [key]
    public int id;
    [foreign Jefe(id)]
    public int jefeId;
    [not null]
    public String name;
}