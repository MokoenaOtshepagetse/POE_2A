using POE_2A;
using System.Collections.Generic;
using System.Windows;

namespace POE_2A
{
    public partial class RecipeListWindow : Window
    {
        private List<Recipe> recipes;

        public RecipeListWindow(List<Recipe> recipes)
        {
            InitializeComponent();
            this.recipes = recipes;
            recipeListBox.ItemsSource = recipes;
        }

        private void ViewRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            Recipe selectedRecipe = (Recipe)recipeListBox.SelectedItem;
            if (selectedRecipe != null)
            {
                RecipeDetailsWindow detailsWindow = new RecipeDetailsWindow(selectedRecipe);
                detailsWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a recipe to view.", "No Recipe Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
