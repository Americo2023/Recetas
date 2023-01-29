import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate, useParams } from "react-router-dom";
import { getRecipesByCategory } from "../../AppData";
import {
	gettingRecipeAction,
	gotRecipeAction,
} from "../../Store/Actions/actions";
import { AppState } from "../../Store/state";

const Recipes = () => {
	const navigate = useNavigate();
	const { id } = useParams();
	const dispatch = useDispatch();
	const recipes = useSelector((state: AppState) => state.recipes.recipes);

	useEffect(() => {
		let cancelled = false;
		let doGetRecipes = async (typeId: number) => {
			dispatch(gettingRecipeAction());
			const recipes = await getRecipesByCategory(typeId);
			if (!cancelled) {
				dispatch(gotRecipeAction(recipes));
			}
		};

		doGetRecipes(Number(id));
		return () => {
			cancelled = true;
		};
	}, [dispatch, id]);

	console.log(recipes);

	const handleClick = (id: number) => {
		navigate(`/recipe/${id}`);
	};

	return (
		<>
			<h1>Recept</h1>
			<div className="thumbnail_container">
				{recipes.map((r) => (
					<div key={r.recipe_id} className="gap-16">
						<div
							className="thumbnail"
							style={{
								backgroundImage: `url("/Assets/${r.recipe_img}")`,
							}}
							onClick={() => handleClick(r.recipe_id)}
						></div>
						<div className="captions">
							<h4>{r.recipe_name}</h4>
							<div>
								<i className="bi bi-stopwatch"></i>
								<p>{r.prep_time} min</p>
							</div>
						</div>
					</div>
				))}
			</div>
		</>
	);
};

export default Recipes;
