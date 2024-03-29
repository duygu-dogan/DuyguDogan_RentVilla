import { CButton, CFormInput } from '@coreui/react'
import axios from 'axios';
import React, { useState } from 'react'
import { ToastContainer, toast } from 'react-toastify';
import ModalComponent from '../modals/ModalComponent';

const FileUploadComponent = ({ fileLabel, uploadUrl, modalButtonColor }) => {
    const [selectedFiles, setSelectedFiles] = useState(null);
    const [filePreviewUrls, setFilePreviewUrls] = useState([]);

    const handleFileChange = (e) => {
        setSelectedFiles(e.target.files);
        console.log(e.target.files)
        const urls = [];
        for (let i = 0; i < e.target.files.length; i++) {
            const file = e.target.files[i];
            urls.push(URL.createObjectURL(file));
        }
        setFilePreviewUrls(urls);
    };
    const handleClear = () => {
        setSelectedFiles(null);
        setFilePreviewUrls([]);
    }
    const handleUpload = async () => {
        if (!selectedFiles) return;
        const formData = new FormData();
        for (let i = 0; i < selectedFiles.length; i++) {
            formData.append('files', selectedFiles[i]);
        }
        try {
            const response = await axios.post(uploadUrl, formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            });
            toast("Files uploaded successfully", { type: 'success' });
        } catch (error) {
            toast("Error uploading files", { type: 'error' });
        }
    };
    return (
        <div>
            <ToastContainer position='bottom-right' />
            <div className="mb-2">
                <CFormInput type="file" id="formFileMultiple" label={fileLabel} multiple onChange={handleFileChange} />
            </div>
            <div className='mb-2'>
                {filePreviewUrls.map((url, index) => (
                    <img key={index} src={url} alt={`Preview ${index}`} style={{ maxWidth: '100px', maxHeight: '100px', marginRight: '10px' }} />)
                )}
            </div>
            <div className='d-flex gap-2'>
                <ModalComponent buttonColor={modalButtonColor} buttonTitle={"Upload"} modalBody={"You are about to upload files. Are you sure?"} onModalClick={handleUpload} />
                <CButton className='btn btn-secondary' onClick={handleClear}>Clear</CButton>
            </div>
        </div>
    )
}

export default FileUploadComponent