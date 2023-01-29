import {
	GETTINGAMOUNT,
	gettingAmountAction,
	GOTAMOUNT,
	gotAmountAction,
} from "../Actions/actions";
import { initialAmountState } from "../state";

type amountActions =
	| ReturnType<typeof gettingAmountAction>
	| ReturnType<typeof gotAmountAction>;

export const amountReducer = (
	state = initialAmountState,
	action: amountActions
) => {
	switch (action.type) {
		case GETTINGAMOUNT: {
			return {
				...state,
				loading: true,
			};
		}
		case GOTAMOUNT: {
			return {
				...state,
				amounts: action.amounts,
				loading: false,
			};
		}
	}

	return state;
};
