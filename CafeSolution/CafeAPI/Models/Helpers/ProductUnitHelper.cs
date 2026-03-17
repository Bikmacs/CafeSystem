namespace CafeAPI.Models.Helpers;

public static class ProductUnitHelper
{
    public static readonly Dictionary<Products, UnitTypes> GetProductUnits = new()
    {
        { Products.Говядина, UnitTypes.кг },
        { Products.Свинина, UnitTypes.кг },
        { Products.Курица, UnitTypes.грамм },
        { Products.Индейка, UnitTypes.грамм },
        { Products.Фарш, UnitTypes.грамм },
        
        { Products.Молоко, UnitTypes.литр },
        { Products.Сметана, UnitTypes.грамм },      
        { Products.Творог, UnitTypes.грамм },
        { Products.Сыр, UnitTypes.грамм },
        { Products.СливочноеМасло, UnitTypes.грамм },
        
        { Products.Мука, UnitTypes.грамм },
        { Products.Сахар, UnitTypes.грамм },
        { Products.Соль, UnitTypes.грамм },
        { Products.ПодсолнечноеМасло, UnitTypes.литр },
        { Products.Рис, UnitTypes.грамм },
        { Products.Гречка, UnitTypes.грамм },
        { Products.МакаронныеИзделия, UnitTypes.грамм },
        
        { Products.Картофель, UnitTypes.грамм },
        { Products.ЛукРепчатый, UnitTypes.грамм },
        { Products.Морковь, UnitTypes.грамм },
        { Products.Капуста, UnitTypes.грамм },
        { Products.Свекла, UnitTypes.грамм },
        
        { Products.ХлебПшеничный, UnitTypes.штук }, 
        { Products.ХлебРжаной, UnitTypes.штук },
        { Products.Батон, UnitTypes.штук },
    };
}