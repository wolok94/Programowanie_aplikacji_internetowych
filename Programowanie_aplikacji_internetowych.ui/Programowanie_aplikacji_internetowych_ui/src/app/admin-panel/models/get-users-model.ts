import { RoleModel } from "./role-model";

export interface GetUsersModel{
    id: string,
    userName: string,
    firstName: string,
    lastName: string,
    role: RoleModel
}