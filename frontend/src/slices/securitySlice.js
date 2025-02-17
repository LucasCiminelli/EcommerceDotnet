import { createSlice } from "@reduxjs/toolkit";
import {
  login,
  register,
  update,
  updatePassword,
  loadUser,
} from "../actions/userAction";
import { saveAddressInfo } from "../actions/cartAction";

const initialState = {
  loading: false,
  errors: [],
  isAuthenticated: false,
  user: null,
  isUpdated: false,
  address: null,
};

export const securitySlice = createSlice({
  name: "security",
  initialState,
  reducers: {
    logout: (state, action) => {
      localStorage.removeItem("token");
      state.isAuthenticated = false;
      state.user = null;
      state.errors = [];
      state.loading = false;
      state.address = null;
    },
    resetUpdateStatus: (state, action) => {
      state.isUpdated = false;
    },
  },
  extraReducers: {
    [login.pending]: (state) => {
      state.loading = true;
      state.errors = [];
    },
    [login.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.errors = [];
      state.user = payload;
      state.isAuthenticated = true;
      state.address = payload.direccionEnvio;
    },
    [login.rejected]: (state, action) => {
      state.loading = false;
      state.errors = action.payload;
      state.isAuthenticated = false;
      state.user = null;
    },

    [register.pending]: (state) => {
      state.loading = true;
      state.errors = [];
    },
    [register.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.errors = [];
      state.user = payload;
      state.isAuthenticated = true;
    },
    [register.rejected]: (state, action) => {
      state.loading = false;
      state.errors = action.payload;
      state.user = null;
      state.isAuthenticated = false;
    },

    [update.pending]: (state) => {
      state.loading = true;
      state.errors = [];
    },
    [update.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.errors = [];
      state.user = payload;
      state.isAuthenticated = true;
      state.isUpdated = true;
    },
    [update.rejected]: (state, action) => {
      state.loading = false;
      state.errors = action.payload;
      state.user = null;
      state.isAuthenticated = false;
    },

    [updatePassword.pending]: (state) => {
      state.loading = true;
      state.errors = [];
    },
    [updatePassword.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.errors = [];
      state.isUpdated = true;
    },
    [updatePassword.rejected]: (state, action) => {
      state.loading = false;
      state.errors = action.payload;
      state.user = null;
      state.isAuthenticated = false;
      state.isUpdated = false;
    },

    [loadUser.pending]: (state) => {
      state.loading = true;
      state.errors = [];
    },
    [loadUser.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.user = payload;
      state.address = payload.direccionEnvio;
      state.errors = [];
      state.isAuthenticated = true;
    },
    [loadUser.rejected]: (state, action) => {
      state.loading = false;
      state.errors = action.payload;
      state.user = null;
      state.isAuthenticated = false;
    },

    [saveAddressInfo.pending]: (state) => {
      state.loading = true;
      state.errors = [];
    },
    [saveAddressInfo.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.isUpdated = true;
      state.address = payload;
      state.errors = [];
    },
    [saveAddressInfo.rejected]: (state, action) => {
      state.loading = false;
      state.errors = action.payload;
      state.user = null;
      state.isAuthenticated = false;
    },
  },
});

export const { logout, resetUpdateStatus } = securitySlice.actions;
export const securityReducer = securitySlice.reducer;
