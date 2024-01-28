import { configureStore } from "@reduxjs/toolkit";
import { combineReducers } from "redux";
import thunk from "redux-thunk";
import { AuthReducer } from "./reducers/AuthReducer";

//reducer - component that stores data
export const rootReducer = combineReducers({
    auth: AuthReducer
});
export type RootState = ReturnType<typeof rootReducer>;


export const store = configureStore({
    reducer: rootReducer,
    devTools: true,
    middleware: [thunk] //changes will occur async
});
export type AppDispatch = typeof store.dispatch;