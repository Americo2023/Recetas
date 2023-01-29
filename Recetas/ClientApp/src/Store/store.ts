import {
	combineReducers,
	legacy_createStore as createStore,
	Store,
} from "redux";
import { amountReducer } from "./Reducers/Amount";
import { categoryReducer } from "./Reducers/Category";
import { maltidReducer } from "./Reducers/Maltid";
import { recipesReducer, recipeReducer } from "./Reducers/Recipe";
import { stepReducer } from "./Reducers/Step";
import { AppState } from "./state";

const rootReducer = combineReducers<AppState>({
	maltid: maltidReducer,
	categories: categoryReducer,
	recipes: recipesReducer,
	recipe: recipeReducer,
	steps: stepReducer,
	amounts: amountReducer,
});

export function configureStore(): Store<AppState> {
	const store = createStore(rootReducer, undefined);
	return store;
}
