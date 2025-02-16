import React, { Fragment, useEffect, useState } from "react";
import MetaData from "./layout/MetaData";
import { useDispatch, useSelector } from "react-redux";
import { getProductsPagination } from "../actions/productAction";
import { useAlert } from "react-alert";
import Products from "./products/Products";
import Pagination from "react-js-pagination";
import {
  setPageIndex,
  updateCategory,
  updatePrecio,
  updateRating,
} from "../slices/productsPaginationSlice";
import Slider from "rc-slider";
import "rc-slider/assets/index.css";

const { createSliderWithTooltip } = Slider;
const Range = createSliderWithTooltip(Slider.Range);

export const Home = () => {
  const [precio, setPrecio] = useState([1, 10000]);
  const dispatch = useDispatch();

  const { categories } = useSelector((state) => state.categories);

  // const { products, loading, error } = useSelector((state) => state.products);

  const {
    products,
    count,
    pageIndex,
    loading,
    error,
    search,
    pageSize,
    precioMax,
    precioMin,
    category,
    rating,
  } = useSelector((state) => state.productsPagination);

  const alert = useAlert();

  useEffect(() => {
    if (error != null) {
      return alert.error(error);
    }

    dispatch(
      getProductsPagination({
        pageIndex: pageIndex,
        pageSize: pageSize,
        search: search,
        precioMax: precioMax,
        precioMin: precioMin,
        categoryId: category,
        rating: rating,
      })
    );
  }, [
    dispatch,
    error,
    alert,
    search,
    pageIndex,
    pageSize,
    precioMax,
    precioMin,
    category,
    rating,
  ]);

  const setCurrentPageNo = (pageNumber) => {
    dispatch(setPageIndex({ pageIndex: pageNumber }));
  };

  function onChangePrecio(precioEvent) {
    setPrecio(precioEvent);
  }

  function onAfterChange(precioEvent) {
    dispatch(updatePrecio({ precio: precioEvent }));
  }
  function onChangeCategory(categoryParam) {
    console.log("Categoría seleccionada:", categoryParam.id);
    dispatch(updateCategory({ category: categoryParam.id }));
  }

  function onChangeStar(starParam) {
    dispatch(updateRating({ rating: starParam }));
  }

  return (
    <Fragment>
      <MetaData titulo={"Los mejores productos online"} />
      <section id="products" className="container mt-5">
        <div className="row">
          {search ? (
            <React.Fragment>
              <div className="col-6 col-md-3 mt-5 mb-5">
                <div className="px-5">
                  <Range
                    marks={{ 1: "$1", 10000: "$10000" }}
                    min={1}
                    max={10000}
                    defaultValue={[1, 10000]}
                    tipFormatter={(value) => `$${value}`}
                    value={precio}
                    tipProps={{ placement: "top", visible: true }}
                    onChange={onChangePrecio}
                    onAfterChange={onAfterChange}
                  />
                </div>

                <hr className="my-5" />

                <div className="mt-5">
                  <h4 className="mb-3"> Categorías </h4>
                  <ul className="pl-0">
                    {categories.map((category) => (
                      <li
                        style={{ cursor: "pointer", listStyleType: "none" }}
                        key={category.id}
                        onClick={() => onChangeCategory(category)}
                      >
                        {category.nombre}
                      </li>
                    ))}
                  </ul>
                </div>

                <hr className="my-5" />

                <div className="mt-5">
                  <h4 className="mb-3">Ratings</h4>
                  <ul className="pl-0">
                    {[5, 4, 3, 2, 1].map((star) => (
                      <li
                        style={{ cursor: "pointer", listStyleType: "none" }}
                        key={star}
                        onClick={() => onChangeStar(star)}
                      >
                        <div className="rating-outer">
                          <div
                            className="rating-inner"
                            style={{ width: `${star * 20}%` }}
                          ></div>
                        </div>
                      </li>
                    ))}
                  </ul>
                </div>
              </div>
              <div className="col-6 col-md-9">
                <div className="row">
                  <Products col={4} products={products} loading={loading} />
                </div>
              </div>
            </React.Fragment>
          ) : (
            <Products col={4} products={products} loading={loading} />
          )}
        </div>
      </section>
      <div className="d-flex justify-content-center mt-5">
        <Pagination
          activePage={pageIndex}
          itemsCountPerPage={pageSize}
          totalItemsCount={count}
          onChange={setCurrentPageNo}
          nextPageText={">"}
          prevPageText={"<"}
          firstPageText={"<<"}
          lastPageText={">>"}
          item-class="page-item"
          linkClass="page-link"
        />
      </div>
    </Fragment>
  );
};
