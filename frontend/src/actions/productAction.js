import { createAsyncThunk, isRejectedWithValue } from "@reduxjs/toolkit";
import axios from "../utilities/axios";
import { delayedTimeout } from "../utilities/delayedTimeout";

export const getProducts = createAsyncThunk(
  "products/getProducts",
  async (ThunkApi, { isRejectedWithValue }) => {
    try {
      await delayedTimeout(1000);
      return await axios.get(`/api/v1/product/list`);
    } catch (err) {
      return isRejectedWithValue(`Errores: ${err.message}`);
    }
  }
);
