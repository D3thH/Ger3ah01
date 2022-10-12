import axios  from "axios";

const baseUrl = 'https://localhost:5001/api/Ger3ah/'

export const ger3ahApi = {
    getNumberOfNameLeft: () => {
       return axios.get(`${baseUrl}GetAllGer3ahNames`);
    },
    pickAName: (name, email) => {
        return axios.get(`${baseUrl}NamePicker?name=${name}&email=${email}`)
    },
    getGer3ahHestory: (name) => {
        return axios.get(`${baseUrl}GetGer3ahHestory?name=${name}`)
    },
    reBuildTheGer3ah:() => {
        return axios.get(`${baseUrl}ReBuildTheGer3ah`)
    },
    removeNameFromGer3ah:(nameThatWillRemoved) => {
        return axios.get(`${baseUrl}RemoveNameFromGer3ah?name=${nameThatWillRemoved}`)
    }
};