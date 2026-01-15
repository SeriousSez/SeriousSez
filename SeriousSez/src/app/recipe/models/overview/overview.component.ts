import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common'
import { Recipe } from '../models/recipe.interface';
import { RecipeService } from '../services/recipe.service';
import { Router } from '@angular/router';
import { UserService } from 'src/app/shared/services/user.service';
import { FavoriteService } from 'src/app/shared/services/favorite.service';
import { Favorites } from 'src/app/shared/models/favorites.interface';
import { Subscription } from 'rxjs';
import { Ingredient } from '../models/ingredient.interface';
import { GroceryService } from 'src/app/shared/services/grocery.service';
import { UserSettings } from 'src/app/account/models/user-settings.interface';

@Component({
    selector: 'app-overview',
    templateUrl: './overview.component.html',
    styleUrls: ['./overview.component.css'],
    standalone: false
})
export class OverviewComponent implements OnInit {
  public recipeList: Recipe[] = [];
  public groceryList: Ingredient[] = [];

  public shownRecipes: Recipe[] = [];
  public recipes: Recipe[];
  public favoredRecipes: Recipe[];
  public selectedRecipes: Recipe[] = [];
  public showFavorites: boolean = false;

  public sortSetting: string = 'created';
  public ascending: boolean = true;

  public loading: boolean = true;
  status: boolean = false;
  settings: UserSettings;
  subscription?: Subscription;
  settingsSubscription?: Subscription;

  constructor(private recipeService: RecipeService, private userService: UserService, private favoriteService: FavoriteService, private groceryService: GroceryService, private datepipe: DatePipe, private router: Router) { }

  ngOnInit(): void {
    this.getRecipes();
    this.getFavorites();
    this.getGroceryLists();
    this.subscription = this.userService.authNavStatus$.subscribe(status => this.status = status);
    this.settingsSubscription = this.userService.settings$.subscribe(settings => this.settings = settings);
  }

  getGroceryLists(){
    this.recipeList = this.groceryService.getRecipeList();
    this.groceryList = this.groceryService.getIngredientList();
  }

  ngOnDestroy() {
    // prevent memory leak when component is destroyed
    this.subscription?.unsubscribe();
  }

  getRecipes(){
    this.recipeService.getRecipes().subscribe((recipes: Recipe[]) => {
      this.recipes = recipes;
      this.shownRecipes = recipes;
      this.loading = false;
      this.sort(this.sortSetting);
    },
    error => {
      //this.notificationService.printErrorMessage(error);
    });
  }

  getFavorites(){
    var username = this.userService.getUserName();
    if(username.length == 0 || username == '' || username == null) return;
    
    this.favoriteService.get(username).subscribe((favorites: Favorites) => {
      this.favoredRecipes = favorites.Recipes;
    },
    error => {
      //this.notificationService.printErrorMessage(error);
    });
  }

  addSelectedRecipesToGroceryList(){
    this.selectedRecipes.forEach(recipe => {
      this.groceryService.toggleRecipeToList(recipe);
      this.recipeList = this.groceryService.getRecipeList();
    });
  }

  toggleRecipeSelected(recipe: Recipe){
    var index = this.selectedRecipes.indexOf(recipe, 0);
    if (index > -1) {
      this.selectedRecipes.splice(index, 1);
    }else{
      this.selectedRecipes.push(recipe);
    }
  }

  toggleDisplay(){
    this.showFavorites = !this.showFavorites;
    if(this.showFavorites){
      if(this.favoredRecipes == null) this.favoredRecipes = [];

      this.shownRecipes = this.favoredRecipes;
    }else{
      this.shownRecipes = this.recipes;
    }
  }

  openRecipe(recipe: Recipe){
    this.router.navigate([`recipe/${recipe.Title.toLocaleLowerCase()}/${recipe.Creator.toLocaleLowerCase()}`]);
  }

  displayDateOnly(created: string){
    return this.datepipe.transform(created, 'dd-MM-yyyy');
  }

  sort(sortSetting: string){
    if(this.sortSetting != sortSetting) this.ascending = true;
    this.sortSetting = sortSetting;

    switch(sortSetting){
      case 'title':
        this.shownRecipes.sort((a, b) => this.ascending == true ? a.Title.localeCompare(b.Title) : -a.Title.localeCompare(b.Title));
        this.ascending = !this.ascending;
        return;
      case 'instructions':
        this.shownRecipes.sort((a, b) => this.ascending == true ? a.Instructions.localeCompare(b.Instructions) : -a.Instructions.localeCompare(b.Instructions));
        this.ascending = !this.ascending;
        return;
      case 'creator':
        this.shownRecipes.sort((a, b) => this.ascending == true ? a.Creator.localeCompare(b.Creator) : -a.Instructions.localeCompare(b.Creator));
        this.ascending = !this.ascending;
        return;
      case 'created':
        this.shownRecipes.sort((a, b) => this.ascending == true ? a.Created.localeCompare(b.Created) : -a.Created.localeCompare(b.Created));
        this.ascending = !this.ascending;
        return;
    }
  }
}
