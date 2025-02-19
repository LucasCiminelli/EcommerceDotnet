import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "../utilities/axios";

export const getCountries = createAsyncThunk(
  "country/getCountries",
  async (ThunkApi, { rejectWithValue }) => {
    try {
      const { data } = await axios.get(`/api/v1/Country`);

      return data;
    } catch (error) {
      return rejectWithValue(error.response.data.message);
    }
  }
);
