import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { getCategories } from "../../AppData";
import {
	gettingCategoriesAction,
	gotCategoriesAction,
} from "../../Store/Actions/actions";
import { AppState } from "../../Store/state";

const Categories = () => {
	const navigate = useNavigate();
	const dispatch = useDispatch();
	const categories = useSelector(
		(state: AppState) => state.categories.categories
	);

	useEffect(() => {
		let cancelled = false;
		let doGetCategories = async () => {
			dispatch(gettingCategoriesAction());
			const categories = await getCategories();
			if (!cancelled) {
				dispatch(gotCategoriesAction(categories));
			}
		};

		doGetCategories();
		return () => {
			cancelled = true;
		};
	}, [dispatch]);

	const handleClick = (id: number) => {
		navigate(`recipes/${id}`);
	};
	console.log(categories);

	return (
		<>
			<h1>Kategorier</h1>
			<div className="thumbnail_container">
				{categories.map((cat) => (
					<div
						key={cat.category_id}
						className="thumbnail gap-16"
						style={{
							backgroundImage: `linear-gradient(
							rgba(0, 0, 0, 0.6), 
							rgba(0, 0, 0, 0.4)
							),url("/Assets/${cat.category_img}")`,
						}}
						onClick={() => handleClick(cat.category_id)}
					>
						<figcaption>{cat.category_name}</figcaption>
					</div>
				))}
			</div>
		</>
	);
};

export default Categories;
