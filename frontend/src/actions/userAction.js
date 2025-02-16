import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "../utilities/axios";
import { delayedTimeout } from "../utilities/delayedTimeout";

export const login = createAsyncThunk(
  "user/login",
  async (params, { rejectWithValue }) => {
    try {
      const requestConfig = {
        headers: {
          "Content-type": "application/json",
        },
      };

      const { data } = await axios.post(
        "/api/v1/Usuario/login",
        params,
        requestConfig
      );

      localStorage.setItem("token", data.token);

      await delayedTimeout(500);

      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  }
);

export const register = createAsyncThunk(
  "user/register",
  async (params, { rejectWithValue }) => {
    try {
      const requestConfig = {
        headers: {
          "Content-type": "multipart/form-data",
        },
      };
      const { data } = await axios.post(
        "/api/v1/Usuario/register",
        params,
        requestConfig
      );

      localStorage.setItem("token", data.token);

      await delayedTimeout(500);

      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  }
);

export const update = createAsyncThunk(
  "user/update",
  async (params, { rejectWithValue }) => {
    try {
      const requestConfig = {
        headers: {
          "Content-type": "multipart/form-data",
        },
      };
      const { data } = await axios.post(
        "/api/v1/Usuario/update",
        params,
        requestConfig
      );

      localStorage.setItem("token", data.token);

      await delayedTimeout(500);

      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  }
);

export const loadUser = createAsyncThunk(
  "user/current",
  async ({ rejectWithValue }) => {
    try {
      const { data } = await axios.get("/api/v1/Usuario");

      localStorage.setItem("token", data.token);

      await delayedTimeout(500);

      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  }
);

export const updatePassword = createAsyncThunk(
  "user/updatePassword",
  async (params, { rejectWithValue }) => {
    try {
      const requestConfig = {
        headers: {
          "Content-type": "application/json",
        },
      };

      const { data } = await axios.put(
        "/api/v1/Usuario/updatepassword",
        params,
        requestConfig
      );

      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  }
);

export const forgotPassword = createAsyncThunk(
  "user/forgotPassword",
  async (params, { rejectWithValue }) => {
    try {
      const requestConfig = {
        headers: {
          "Content-type": "application/json",
        },
      };

      const { data } = await axios.post(
        "/api/v1/Usuario/forgotpassword",
        params,
        requestConfig
      );

      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  }
);

export const resetPassword = createAsyncThunk(
  "user/resetPassword",
  async ({ email, password, confirmPassword, token }, { rejectWithValue }) => {
    try {
      const requestConfig = {
        headers: {
          "Content-type": "application/json",
        },
      };

      const request = {
        email,
        password,
        confirmPassword,
        token,
      };

      const { data } = await axios.post(
        "/api/v1/Usuario/resetpassword",
        request,
        requestConfig
      );

      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  }
);
