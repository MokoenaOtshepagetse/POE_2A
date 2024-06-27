using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace POE_2A
{
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes;
        private Recipe currentRecipe;

        public MainWindow()
        {
            InitializeComponent();
            recipes = new List<Recipe>();
        }

        private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            currentRecipe = new Recipe();
            recipePanel.Visibility = Visibility.Visible;
            inputPanel.Visibility = Visibility.Visible;
            displayPanel.Visibility = Visibility.Collapsed;
            recipeTitle.Text = "Add New Recipe";
            listRecipesButton.IsEnabled = true;

            PromptForRecipeName();
        }

        private void PromptForRecipeName()
        {
            currentRecipe.Name = Microsoft.VisualBasic.Interaction.InputBox("Enter the recipe name:", "Recipe Name");
            PromptForNumberOfIngredients();
        }

        private void PromptForNumberOfIngredients()
        {
            if (int.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Enter the number of ingredients:", "Number of Ingredients"), out int numIngredients))
            {
                currentRecipe.NumIngredients = numIngredients;
                PromptForIngredients(numIngredients);
            }
        }

        private void PromptForIngredients(int numIngredients)
        {
            currentRecipe.Ingredients = new List<Ingredient>();

            for (int i = 0; i < numIngredients; i++)
            {
                string name = Microsoft.VisualBasic.Interaction.InputBox($"Enter the name of ingredient {i + 1}:", "Ingredient Name");
                double quantity = double.Parse(Microsoft.VisualBasic.Interaction.InputBox($"Enter the quantity of ingredient {i + 1}:", "Ingredient Quantity"));
                string unit = Microsoft.VisualBasic.Interaction.InputBox($"Enter the unit of ingredient {i + 1}:", "Ingredient Unit");
                double calories = double.Parse(Microsoft.VisualBasic.Interaction.InputBox($"Enter the calories of ingredient {i + 1}:", "Ingredient Calories"));

                currentRecipe.Ingredients.Add(new Ingredient { Name = name, Quantity = quantity, Unit = unit, Calories = calories });
            }

            PromptForNumberOfSteps();
        }

        private void PromptForNumberOfSteps()
        {
            if (int.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Enter the number of steps:", "Number of Steps"), out int numSteps))
            {
                currentRecipe.NumSteps = numSteps;
                PromptForSteps(numSteps);
            }
        }

        private void PromptForSteps(int numSteps)
        {
            currentRecipe.Steps = new List<string>();

            for (int i = 0; i < numSteps; i++)
            {
                string step = Microsoft.VisualBasic.Interaction.InputBox($"Enter step {i + 1}:", "Recipe Step");
                currentRecipe.Steps.Add(step);
            }

            SaveRecipe();
        }

        private void SaveRecipe()
        {
            recipes.Add(currentRecipe);

            // Display recipe details
            recipeTitle.Text = currentRecipe.Name;
            ingredientDisplayList.ItemsSource = currentRecipe.Ingredients;
            stepDisplayList.ItemsSource = currentRecipe.Steps;
            totalCalories.Text = currentRecipe.Ingredients.Sum(ingredient => ingredient.Calories).ToString();

            inputPanel.Visibility = Visibility.Collapsed;
            displayPanel.Visibility = Visibility.Visible;
        }

        private void ListRecipesButton_Click(object sender, RoutedEventArgs e)
        {
            RecipeListWindow recipeListWindow = new RecipeListWindow(recipes.OrderBy(r => r.Name).ToList());
            recipeListWindow.Show();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Enter the multiplier:", "Multiply Measurements"), out double multiplier))
            {
                foreach (var ingredient in currentRecipe.Ingredients)
                {
                    ingredient.Quantity *= multiplier;
                }

                ingredientDisplayList.ItemsSource = null;
                ingredientDisplayList.ItemsSource = currentRecipe.Ingredients;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            recipePanel.Visibility = Visibility.Collapsed;
            inputPanel.Visibility = Visibility.Collapsed;
            displayPanel.Visibility = Visibility.Collapsed;
        }
    }

    public class Recipe
    {
        public string Name { get; set; }
        public int NumIngredients { get; set; }
        public int NumSteps { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Steps { get; set; }
    }

    public class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public double Calories { get; set; }

        public override string ToString()
        {
            return $"{Name}: {Quantity} {Unit} ({Calories} calories)";
        }
    }
}
