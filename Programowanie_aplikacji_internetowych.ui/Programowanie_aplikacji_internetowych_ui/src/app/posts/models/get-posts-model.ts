import { MetaDataModel } from "../../shared/models/metadata-model";

export interface GetPostsModel{
    title: string;
    text: string;
    imageUrl: string;
    metaData: MetaDataModel;
    numberOfComments: number;
}