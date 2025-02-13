import { createAsyncThunk, isRejectedWithValue } from "@reduxjs/toolkit";
import axios from "../utilities/axios";

export const getProducts = createAsyncThunk(
  "products/getProducts",
  async (ThunkApi, { isRejectedWithValue }) => {
    try {
      return await axios.get(`/api/v1/product/list`);
    } catch (err) {
      return isRejectedWithValue(`Errores: ${err.message}`);
    }
  }
);
