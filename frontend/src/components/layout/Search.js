import React, { useState } from "react";
import {
  resetPagination,
  searchPagination,
} from "../../slices/productsPaginationSlice";
import { useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux";

const Search = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const [keyword, setKeyword] = useState("");

  const searchHandler = (e) => {
    e.preventDefault();

    if (keyword.trim()) {
      dispatch(searchPagination({ search: keyword }));
    } else {
      dispatch(resetPagination());
    }

    navigate("/");
  };

  return (
    <form onSubmit={searchHandler}>
      <div className="input-group">
        <input
          type="text"
          className="form-control"
          placeholder="Buscá tu producto..."
          onChange={(e) => setKeyword(e.target.value)}
        />
        <div className="input-group-append">
          <button id="search_btn" className="btn">
            <i className="fa fa-search" aria-hidden="true"></i>
          </button>
        </div>
      </div>
    </form>
  );
};

export default Search;
