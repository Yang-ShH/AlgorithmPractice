using BlazorComponent;

namespace MasaBlazorAppPractice.Pages
{
    public partial class FetchData
    {
        private List<DataTableHeader<Dessert>> _headers = new List<DataTableHeader<Dessert>>()
        {
            new DataTableHeader<Dessert>()
            {
                Text = "Dessert",
                Align = "start",
                Sortable = false,
                Value = nameof(Dessert.Name),
            },
            new DataTableHeader<Dessert>(){Text = "Calories", Value = nameof(Dessert.Calories)},
            new DataTableHeader<Dessert>(){Text = "Fat", Value = nameof(Dessert.Fat)},
            new DataTableHeader<Dessert>(){Text = "Carbs", Value = nameof(Dessert.Carbs)},
            new DataTableHeader<Dessert>(){Text = "Protein", Value = nameof(Dessert.Protein)},
            new DataTableHeader<Dessert>(){Text = "Iron", Value = nameof(Dessert.Iron)},
        };

        private List<Dessert> _desserts = new List<Dessert> { };

        protected override Task OnInitializedAsync()
        {
            GetList();
            return base.OnInitializedAsync();
        }

        private void GetList()
        {
            _desserts = new List<Dessert>
            {
                new Dessert() {Name = "q", Calories = 111, Fat = 1.0, Carbs = 11, Protein = 11.1, Iron = "1%"},
                new Dessert() {Name = "w", Calories = 112, Fat = 2.0, Carbs = 12, Protein = 12.1, Iron = "2%"},
                new Dessert() {Name = "e", Calories = 113, Fat = 3.0, Carbs = 13, Protein = 13.1, Iron = "3%"},
                new Dessert() {Name = "r", Calories = 114, Fat = 4.0, Carbs = 14, Protein = 14.1, Iron = "4%"},
                new Dessert() {Name = "t", Calories = 115, Fat = 5.0, Carbs = 15, Protein = 15.1, Iron = "5%"},
                new Dessert() {Name = "y", Calories = 116, Fat = 6.0, Carbs = 16, Protein = 16.1, Iron = "6%"},
                new Dessert() {Name = "u", Calories = 117, Fat = 7.0, Carbs = 17, Protein = 17.1, Iron = "7%"},
                new Dessert() {Name = "i", Calories = 118, Fat = 8.0, Carbs = 18, Protein = 18.1, Iron = "8%"},
                new Dessert() {Name = "o", Calories = 119, Fat = 9.0, Carbs = 19, Protein = 19.1, Iron = "9%"},
            };
        }
    }

    public class Dessert
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public double Fat { get; set; }
        public short Carbs { get; set; }
        public double Protein { get; set; }
        public string Iron { get; set; }
    }
}
