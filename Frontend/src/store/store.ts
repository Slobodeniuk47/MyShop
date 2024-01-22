import { configureStore } from "@reduxjs/toolkit";
import { combineReducers } from "redux";
import thunk from "redux-thunk";
import { AuthReducer } from "./reducers/AuthReducer";

export const rootReducer = combineReducers({
    auth: AuthReducer
});

export const store = configureStore({
    reducer: rootReducer,
    devTools: true,
    middleware: [thunk]
});
export type RootState = ReturnType<typeof rootReducer>;
export type AppDispatch = typeof store.dispatch;