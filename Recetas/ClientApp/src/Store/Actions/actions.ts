import { AmountResponseModel } from "../../Models/AmountResponseModel";
import { CategoryModel } from "../../Models/CategoryModel";
import { MaltidModel } from "../../Models/MaltidModel";
import { RecipeModel } from "../../Models/RecipeModel";
import { StepModel } from "../../Models/StepModel";

/* Amount actions */
export const GETTINGAMOUNT = "GettingAmount";
export const gettingAmountAction = () => ({ type: GETTINGAMOUNT } as const);

export const GOTAMOUNT = "GotAmount";
export const gotAmountAction = (amounts: AmountResponseModel[]) =>
	({ type: GOTAMOUNT, amounts: amounts } as const);

/* Cateogry actions */
export const GOTCATEGORIES = "GotCategories";
export const gotCategoriesAction = (categories: CategoryModel[]) =>
	({ type: GOTCATEGORIES, categories: categories } as const);

export const GETTINGCATEGORIES = "GettingCategories";
export const gettingCategoriesAction = () =>
	({ type: GETTINGCATEGORIES } as const);

/* Maltid actions */
export const GOTMALTID = "GotMaltid";
export const gotMaltidAction = (maltid: MaltidModel[]) =>
	({ type: GOTMALTID, maltid: maltid } as const);

export const GETTINGMALTID = "GettingMaltid";
export const gettingMaltidAction = () => ({ type: GETTINGMALTID } as const);

/* Recipe actions */
export const GOTRECIPE = "GotRecipe";
export const gotRecipeAction = (recipes: RecipeModel[]) =>
	({ type: GOTRECIPE, recipes: recipes } as const);

export const GOTONERECIPE = "GotOneRecipe";
export const gotOneRecipeAction = (recipe: RecipeModel) =>
	({ type: GOTONERECIPE, recipe: recipe } as const);

export const GETTINGRECIPE = "GettingRecipe";
export const gettingRecipeAction = () => ({ type: GETTINGRECIPE } as const);

export const GETTINGONERECIPE = "GettingOneRecipe";
export const gettingOneRecipeAction = () =>
	({ type: GETTINGONERECIPE } as const);

/* Step actions */
export const GOTSTEPS = "GotSteps";
export const gotStepsAction = (steps: StepModel[]) =>
	({ type: GOTSTEPS, steps: steps } as const);

export const GETTINGSTEPS = "GettingSteps";
export const gettingStepsAction = () => ({ type: GETTINGSTEPS } as const);
