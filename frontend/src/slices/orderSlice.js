import { createSlice } from "@reduxjs/toolkit";
import { saveOrder } from "../actions/orderAction";

const initialState = {
  loading: false,
  errors: null,
  order: null,
  isUpdated: false,
  paymentIntentId: "",
  clientSecret: "",
  stripeApiKey: "",
};

export const orderSlice = createSlice({
  name: "order",
  initialState,
  reducers: {
    resetUpdateStatus: (state) => {
      state.isUpdated = false;
    },
  },
  extraReducers: {
    [saveOrder.pending]: (state) => {
      state.loading = true;
      state.errors = null;
    },
    [saveOrder.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.errors = null;
      state.isUpdated = true;
      state.order = payload;
      state.stripeApiKey = payload.stripeApiKey;
      state.paymentIntentId = payload.paymentIntentId;
      state.clientSecret = payload.clientSecret;
    },
    [saveOrder.rejected]: (state, action) => {
      state.loading = false;
      state.errors = action.payload;
      state.order = null;
    },
  },
});

export const { resetUpdateStatus } = orderSlice.actions;

export const orderReducer = orderSlice.reducer;
