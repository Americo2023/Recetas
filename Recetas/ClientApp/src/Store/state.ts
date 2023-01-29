import { AmountResponseModel } from "../Models/AmountResponseModel";
import { CategoryModel } from "../Models/CategoryModel";
import { MaltidModel } from "../Models/MaltidModel";
import { RecipeModel } from "../Models/RecipeModel";
import { StepModel } from "../Models/StepModel";

let recipe: RecipeModel = {
	recipe_id: 0,
	amount_id: 0,
	category_id: 0,
	recipe_name: "",
	recipe_description: "",
	prep_time: "",
	recipe_img: "",
};

export interface MaltidState {
	readonly loading: boolean;
	readonly maltid: MaltidModel[];
	readonly searched: MaltidModel[];
}

export interface CategoryState {
	readonly loading: boolean;
	readonly categories: CategoryModel[];
	readonly searched: CategoryModel[];
}

export interface RecipesState {
	readonly loading: boolean;
	readonly recipes: RecipeModel[];
	readonly searched: RecipeModel[];
}

export interface RecipeState {
	readonly loading: boolean;
	readonly recipe: RecipeModel;
	readonly searched: RecipeModel;
}

export interface StepState {
	readonly loading: boolean;
	readonly steps: StepModel[];
	readonly searched: StepModel[];
}

export interface AmountState {
	readonly loading: boolean;
	readonly amounts: AmountResponseModel[];
	readonly searched: AmountResponseModel[];
}

export const initialMaltidState: MaltidState = {
	loading: false,
	maltid: [],
	searched: [],
};

export const initialCategoryState: CategoryState = {
	loading: false,
	categories: [],
	searched: [],
};

export const initialRecipesState: RecipesState = {
	loading: false,
	recipes: [],
	searched: [],
};

export const initialRecipeState: RecipeState = {
	loading: false,
	recipe: recipe,
	searched: recipe,
};

export const initialStepState: StepState = {
	loading: false,
	steps: [],
	searched: [],
};

export const initialAmountState: AmountState = {
	loading: false,
	amounts: [],
	searched: [],
};

export interface AppState {
	readonly maltid: MaltidState;
	readonly categories: CategoryState;
	readonly recipes: RecipesState;
	readonly recipe: RecipeState;
	readonly steps: StepState;
	readonly amounts: AmountState;
}
