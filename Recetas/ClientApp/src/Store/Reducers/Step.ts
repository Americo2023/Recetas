import {
	gettingStepsAction,
	gotStepsAction,
	GETTINGSTEPS,
	GOTSTEPS,
} from "../Actions/actions";
import { initialStepState } from "../state";

type stepActions =
	| ReturnType<typeof gettingStepsAction>
	| ReturnType<typeof gotStepsAction>;

export const stepReducer = (state = initialStepState, action: stepActions) => {
	switch (action.type) {
		case GETTINGSTEPS: {
			return {
				...state,
				loading: true,
			};
		}
		case GOTSTEPS: {
			return {
				...state,
				steps: action.steps,
				loading: false,
			};
		}
	}

	return state;
};
