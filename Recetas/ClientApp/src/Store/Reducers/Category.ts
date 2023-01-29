import {
	GETTINGCATEGORIES,
	GOTCATEGORIES,
	gettingCategoriesAction,
	gotCategoriesAction,
} from "../Actions/actions";
import { initialCategoryState } from "../state";

type categoryActions =
	| ReturnType<typeof gettingCategoriesAction>
	| ReturnType<typeof gotCategoriesAction>;

export const categoryReducer = (
	state = initialCategoryState,
	action: categoryActions
) => {
	switch (action.type) {
		case GETTINGCATEGORIES: {
			return {
				...state,
				loading: true,
			};
		}
		case GOTCATEGORIES: {
			return {
				...state,
				categories: action.categories,
				loading: false,
			};
		}
	}

	return state;
};
