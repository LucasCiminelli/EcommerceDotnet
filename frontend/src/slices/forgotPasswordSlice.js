import { createSlice } from "@reduxjs/toolkit";
import { forgotPassword } from "../actions/userAction";

const initialState = {
  message: null,
  errores: null,
  loading: false,
};

export const forgotPasswordSlice = createSlice({
  name: "forgotPassword",
  initialState,
  reducers: {
    resetErrors: (state, action) => {
      state.errores = null;
      state.message = null;
      state.loading = false;
    },
  },
  extraReducers: {
    [forgotPassword.pending]: (state) => {
      state.message = null;
      state.errores = null;
      state.loading = true;
    },
    [forgotPassword.fulfilled]: (state, { payload }) => {
      state.message = payload.data;
      state.errores = null;
      state.loading = false;
    },
    [forgotPassword.rejected]: (state, action) => {
      state.message = null;
      state.errores = action.payload;
      state.loading = false;
    },
  },
});

export const { resetErrors } = forgotPasswordSlice.actions;
export const forgotPasswordReducer = forgotPasswordSlice.reducer;
