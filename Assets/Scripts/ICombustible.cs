public interface ICombustible
{
    bool IsCombustible { get; } //Воспламеняемый ли объект
    bool IsBurnt { get; }       //Горящий ли объект сейчас
    void SetFire(int serialBurn); //Поджечь объект
}

