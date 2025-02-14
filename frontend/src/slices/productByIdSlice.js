import { createSlice } from "@reduxjs/toolkit";
import { getProductById } from "../actions/productAction";

export const initialState = {
  product: null,
  loading: false,
  error: null,
};

export const productByIdSlice = createSlice({
  name: "productByIdSlice",
  initialState,
  reducers: {
    resetGetById: (state, action) => {
      state.loading = false;
      state.error = null;
      state.product = null;
    },
  },
  extraReducers: {
    [getProductById.pending]: (state) => {
      state.loading = true;
      state.error = null;
    },
    [getProductById.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.error = null;
      state.product = payload.data;
    },
    [getProductById.rejected]: (state, action) => {
      state.loading = false;
      state.error = action.payload;
    },
  },
});

export const { resetGetById } = productByIdSlice.actions;
export const productByIdReducer = productByIdSlice.reducer;
