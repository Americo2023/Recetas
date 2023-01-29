import { http } from "./http";
import { AmountResponseModel } from "./Models/AmountResponseModel";
import { CategoryModel } from "./Models/CategoryModel";
import { MaltidModel } from "./Models/MaltidModel";
import { RecipeModel } from "./Models/RecipeModel";
import { StepModel } from "./Models/StepModel";

export const getMaltid = async (): Promise<MaltidModel[]> => {
	let maltid: MaltidModel[] = [];
	const result = await http<MaltidModel[]>({
		path: "/Maltid",
	});

	if (result.ok && result.body) {
		maltid = result.body;
	}

	return maltid;
};

export const getCategories = async (): Promise<CategoryModel[]> => {
	let categories: CategoryModel[] = [];

	const result = await http<CategoryModel[]>({
		path: "/Category",
	});

	if (result.ok && result.body) {
		categories = result.body;
	}

	return categories;
};

export const getRecipesByCategory = async (
	id: number
): Promise<RecipeModel[]> => {
	let recipes: RecipeModel[] = [];

	const result = await http<RecipeModel[]>({
		path: `/Recipes/getrecipesbycategory/${id}`,
	});

	if (result.ok && result.body) {
		recipes = result.body;
	}

	return recipes;
};

export const getStepsById = async (id: number): Promise<StepModel[]> => {
	let steps: StepModel[] = [];

	const result = await http<StepModel[]>({
		path: `/Step/getstepbyrecipeid/${id}`,
	});

	if (result.ok && result.body) {
		steps = result.body;
	}

	return steps;
};

export const getRecipeById = async (id: number): Promise<RecipeModel> => {
	let recipe: RecipeModel = {
		recipe_id: 0,
		amount_id: 0,
		category_id: 0,
		recipe_name: "",
		recipe_description: "",
		prep_time: "",
		recipe_img: "",
	};

	const result = await http<RecipeModel>({
		path: `/Recipes/${id}`,
	});

	if (result.ok && result.body) {
		recipe = result.body;
	}

	return recipe;
};

export const getAmountsByRecipe = async (
	id: number
): Promise<AmountResponseModel[]> => {
	let response: AmountResponseModel[] = [];

	const result = await http<AmountResponseModel[]>({
		path: `/Amount/amountsbyrecipeid/${id}`,
	});

	if (result.ok && result.body) {
		response = result.body;
	}

	return response;
};
