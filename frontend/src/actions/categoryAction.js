import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "../utilities/axios";

export const getCategories = createAsyncThunk(
  "categories/getCategories",
  async (ThunkApi, { rejectWithValue }) => {
    try {
      return await axios.get("/api/v1/Category");
    } catch (err) {
      return rejectWithValue(`Errores: ${err.message}`);
    }
  }
);
