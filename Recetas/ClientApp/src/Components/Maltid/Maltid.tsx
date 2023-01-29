import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { getMaltid } from "../../AppData";
import {
	gettingMaltidAction,
	gotMaltidAction,
} from "../../Store/Actions/actions";
import { AppState } from "../../Store/state";

const Maltid = () => {
	const navigate = useNavigate();
	const dispatch = useDispatch();
	const maltid = useSelector((state: AppState) => state.maltid.maltid);

	useEffect(() => {
		let cancelled = false;
		let doGetMaltid = async () => {
			dispatch(gettingMaltidAction());
			const maltid = await getMaltid();
			if (!cancelled) {
				dispatch(gotMaltidAction(maltid));
			}
		};

		doGetMaltid();

		return () => {
			cancelled = true;
		};
	}, [dispatch]);

	const handleClick = (id: number) => {
		navigate(`recipes/${id}`);
	};

	return (
		<>
			<h1>Måltid</h1>
			<div className="thumbnail_container">
				{maltid.map((m) => (
					<div
						key={m.maltid_id}
						className="thumbnail gap-16"
						style={{
							backgroundImage: `linear-gradient(
							rgba(0, 0, 0, 0.6), 
							rgba(0, 0, 0, 0.4)
							),url("/Assets/${m.maltid_img}")`,
						}}
						onClick={() => handleClick(m.maltid_id)}
					>
						<figcaption>{m.maltid_name}</figcaption>
					</div>
				))}
			</div>
		</>
	);
};

export default Maltid;
