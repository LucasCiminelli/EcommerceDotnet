import React, { Fragment, useEffect } from "react";
import MetaData from "./layout/MetaData";
import { useDispatch, useSelector } from "react-redux";
import { getProductsPagination } from "../actions/productAction";
import { useAlert } from "react-alert";
import Products from "./products/Products";
import Pagination from "react-js-pagination";
import { setPageIndex } from "../slices/productsPaginationSlice";

export const Home = () => {
  const dispatch = useDispatch();

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

  return (
    <Fragment>
      <MetaData titulo={"Los mejores productos online"} />
      <section id="products" className="container mt-5">
        <div className="row">
          <Products col={4} products={products} loading={loading} />
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
