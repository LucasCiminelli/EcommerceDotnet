import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "../utilities/axios";
import { delayedTimeout } from "../utilities/delayedTimeout";

export const getProducts = createAsyncThunk(
  "products/getProducts",
  async (ThunkApi, { rejectWithValue }) => {
    try {
      await delayedTimeout(1000);
      return await axios.get(`/api/v1/product/list`);
    } catch (err) {
      return rejectWithValue(`Errores: ${err.message}`);
    }
  }
);

export const getProductById = createAsyncThunk(
  "products/getProductById",
  async (id, { rejectWithValue }) => {
    try {
      await delayedTimeout(500);
      return await axios.get(`/api/v1/product/${id}`);
    } catch (err) {
      return rejectWithValue(`Errores : ${err.message}`);
    }
  }
);
