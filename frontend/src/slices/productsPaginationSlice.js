import { createSlice } from "@reduxjs/toolkit";
import { getProductsPagination } from "../actions/productAction";

export const initialState = {
  products: [],
  count: 0, //cantidad total de records que existen
  pageIndex: 1,
  pageSize: 2,
  pageCount: 0, //cantidad de paginas totales generadas por la paginación
  resultByPage: 0, //cantidad de resultados por pagina, es un dato variable. depende de en que pagina te encuentres, en algunos casos.
  search: null,
  precioMax: null,
  precioMin: null,
  category: null,
  rating: null,
  loading: false,
  error: null,
};

export const productPaginationSlice = createSlice({
  name: "getProductsPagination",
  initialState,
  reducers: {
    //reducers locales, actualizan el estado de las variables de estado locales del initialState

    searchPagination: (state, action) => {
      state.search = action.payload.search;
      state.pageIndex = 1;
    },
    setPageIndex: (state, action) => {
      state.pageIndex = action.payload.pageIndex;
    },
    updatePrecio: (state, action) => {
      state.precioMax = action.payload.precio[1];
      state.precioMin = action.payload.precio[0];
    },
    updateCategory: (state, action) => {
      state.category = action.payload.category;
    },
    updateRating: (state, action) => {
      state.rating = action.payload.rating;
    },
    resetPagination: (state, action) => {
      state.precioMax = null;
      state.precioMin = null;
      state.pageIndex = 1;
      state.search = null;
      state.category = null;
      state.rating = null;
    },
  },
  extraReducers: {
    [getProductsPagination.pending]: (state) => {
      state.loading = true;
      state.error = null;
    },
    [getProductsPagination.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.error = null;
      state.products = payload.data;
      state.count = payload.count;
      state.pageCount = payload.pageCount;
      state.pageIndex = payload.pageIndex;
      state.pageSize = payload.pageSize;
      state.resultByPage = payload.resultByPage;
      state.rating = payload.rating;
      state.category = payload.category;
    },
    [getProductsPagination.rejected]: (state, action) => {
      state.loading = false;
      state.error = action.payload;
    },
  },
});

export const {
  searchPagination,
  setPageIndex,
  updateCategory,
  updatePrecio,
  updateRating,
  resetPagination,
} = productPaginationSlice.actions;

export const productsPaginationReducer = productPaginationSlice.reducer;
