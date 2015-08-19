namespace MealService
{
    public interface IDishFactory
    {
        IDish Transform(IDishDto dishDto);
    }
}
