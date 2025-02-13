import { createSlice } from "@reduxjs/toolkit";
import { getProducts } from "../actions/productAction";

export const initialState = {
  products: [],
  loading: false,
  error: null,
};

export const productsSlice = createSlice({
  name: "products",
  initialState,
  reducers: {},
  extraReducers: {
    [getProducts.pending]: (state) => {
      state.loading = true;
      state.error = null;
    },
    [getProducts.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.products = payload.data;
      state.error = null;
    },
    [getProducts.rejected]: (state, action) => {
      state.loading = false;
      state.error = action.payload;
    },
  },
});

export const productsReducer = productsSlice.reducer;
