import { createSlice } from "@reduxjs/toolkit";
import { getCountries } from "../actions/countryAction";

export const initialState = {
  loading: false,
  errors: null,
  countries: [],
};

export const countrySlice = createSlice({
  name: "countries",
  initialState,
  reducers: {},
  extraReducers: {
    [getCountries.pending]: (state) => {
      state.loading = true;
      state.errors = null;
    },
    [getCountries.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.errors = null;
      state.countries = payload;
    },
    [getCountries.rejected]: (state, action) => {
      state.loading = false;
      state.errors = action.payload;
    },
  },
});

export const countryReducer = countrySlice.reducer;
