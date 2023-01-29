import React from "react";
import { Provider } from "react-redux";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Recipe from "./Components/Recipe/Recipe";
import Recipes from "./Components/Recipes/Recipes";
import Thumbnails from "./Components/Thumbnails/Thumbnails";
import "./custom.css";
import { configureStore } from "./Store/store";

const store = configureStore();

export const App: React.FunctionComponent = () => {
	return (
		<BrowserRouter>
			<Provider store={store}>
				<div id="container">
					<Routes>
						<Route path="/" element={<Thumbnails />} />
						<Route path="recipes/:id" element={<Recipes />} />
						<Route path="recipe/:id" element={<Recipe />} />
					</Routes>
				</div>
			</Provider>
		</BrowserRouter>
	);
};
