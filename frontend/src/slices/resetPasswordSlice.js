import { createSlice } from "@reduxjs/toolkit";
import { resetPassword } from "../actions/userAction";

export const initialState = {
  message: null,
  errores: null,
  loading: false,
};

export const resetPasswordSlice = createSlice({
  name: "resetPassword",
  initialState,
  reducers: {
    resetErrors: (state) => {
      state.message = null;
      state.errores = null;
      state.loading = false;
    },
  },
  extraReducers: {
    [resetPassword.pending]: (state) => {
      state.message = null;
      state.loading = true;
      state.errores = null;
    },
    [resetPassword.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.errores = false;
      state.message = payload;
    },
    [resetPassword.rejected]: (state, action) => {
      state.loading = false;
      state.message = null;
      state.errores = action.payload;
    },
  },
});

export const { resetErrors } = resetPasswordSlice.actions;
export const resetPasswordReducer = resetPasswordSlice.reducer;
