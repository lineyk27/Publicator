import Axios from "axios";

function uploadImageByUrl(url){
  var image;
  Axios.get(url)
    .then(response => {
      console.log(response.data);
      console.log(response.data.type);
    });
  return uploadImageByFile(image);
}

// TODO: remove api url's from here
function uploadImageByFile(file){
  var uploadUrl = "https://api.cloudinary.com/v1_1/dgepkksyl/image/upload";
  var form = new FormData();
  form.append("upload_preset", "hunl39ro");
  form.append("file", file);
  return Axios.post(uploadUrl, form, )
    .then(response => {
      return {
        success: 1,
        file: {
          url: response.data.url
        }
      }
    });
}

export {
    uploadImageByFile,
    uploadImageByUrl
}