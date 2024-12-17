import { MetaDataModel } from "../../shared/models/metadata-model";
import { Comment } from "./comment-model";

export interface GetPostByIdModel{
    id: number;             
    title: string;          
    text: string;           
    imageUrl: string;        
    comments: Comment[];    
    metaData: MetaDataModel;     
}