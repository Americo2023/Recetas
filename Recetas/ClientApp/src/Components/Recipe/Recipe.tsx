import { useEffect } from "react";
import { useSelector } from "react-redux";
import { useDispatch } from "react-redux";
import { useParams } from "react-router-dom";
import { getStepsById, getRecipeById, getAmountsByRecipe } from "../../AppData";
import {
	gettingAmountAction,
	gettingOneRecipeAction,
	gettingStepsAction,
	gotAmountAction,
	gotOneRecipeAction,
	gotStepsAction,
} from "../../Store/Actions/actions";
import { AppState } from "../../Store/state";

const Recipe = () => {
	const { id } = useParams();
	const dispatch = useDispatch();
	const steps = useSelector((state: AppState) => state.steps.steps);
	const recipe = useSelector((state: AppState) => state.recipe.recipe);
	const amounts = useSelector((state: AppState) => state.amounts.amounts);

	useEffect(() => {
		let cancelled = false;
		let doGetSteps = async (id: number) => {
			dispatch(gettingStepsAction());
			dispatch(gettingOneRecipeAction());
			dispatch(gettingAmountAction());
			const steps = await getStepsById(id);
			const recipe = await getRecipeById(id);
			const amounts = await getAmountsByRecipe(id);
			if (!cancelled) {
				dispatch(gotStepsAction(steps));
				dispatch(gotOneRecipeAction(recipe));
				dispatch(gotAmountAction(amounts));
			}
		};

		doGetSteps(Number(id));
		return () => {
			cancelled = true;
		};
	}, [dispatch, id]);

	return (
		<>
			<div className="flex-container">
				<div className="flex-items">
					<img src={`/Assets/${recipe.recipe_img}`} alt="recept" />
				</div>
				<div className="flex-items">
					<h1>{recipe.recipe_name}</h1>
					<small>
						{recipe.amount_id} protioner - {recipe.prep_time} min.
					</small>
					<p>{recipe.recipe_description}</p>
				</div>
			</div>
			<div className="flex-container">
				<div className="flex-items">
					<h2>Ingredienser</h2>
					<ul>
						{amounts.map((a) => (
							<li key={a.ingerdient_name}>
								{a.ingredient_amount} {a.measurement_name}{" "}
								{a.ingerdient_name}
							</li>
						))}
					</ul>
				</div>
				<div className="flex-items">
					<h2>Gör så här</h2>
					<ul>
						{steps.map((s) => (
							<li key={s.step_id}>
								{s.step_number} {s.step_description}
							</li>
						))}
					</ul>
				</div>
			</div>
		</>
	);
};

export default Recipe;
