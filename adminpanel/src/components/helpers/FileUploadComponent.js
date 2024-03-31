import { CButton, CFormInput } from '@coreui/react'
import axios from 'axios';
import React, { useState } from 'react'
import { ToastContainer, toast } from 'react-toastify';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faImage } from '@fortawesome/free-solid-svg-icons';

const FileUploadComponent = ({ fileLabel, uploadUrl, id }) => {
    const [selectedFiles, setSelectedFiles] = useState(null);
    const [filePreviewUrls, setFilePreviewUrls] = useState([]);

    const handleFileChange = (e) => {
        setSelectedFiles(e.target.files);
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
        console.log("upload clicked")
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

            <button type="button" className="btn btn-info btn-sm" style={{ fontSize: "15px" }} data-bs-toggle="modal" data-bs-target={`#imageModal${id}`}>
                <FontAwesomeIcon icon={faImage} />
            </button>

            <div className="modal fade" id={`imageModal${id}`} tabindex="-1" aria-labelledby="imageModalLabel" aria-hidden="true">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header p-3">
                            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">

                            <div className="mb-2">
                                <CFormInput type="file" id="formFileMultiple" label={fileLabel} multiple onChange={handleFileChange} />
                            </div>
                            <div className='mb-2'>
                                {filePreviewUrls.map((url, index) => (
                                    <img key={index} src={url} alt={`Preview ${index}`} style={{ maxWidth: '100px', maxHeight: '100px', marginRight: '10px' }} />)
                                )}
                            </div>
                            <div className='modal-footer'>
                                <div className='d-flex gap-2'>
                                    <CButton className='btn btn-primary' data-bs-dismiss="modal" onClick={handleUpload}>Upload</CButton>
                                    <CButton className='btn btn-warning' onClick={handleClear}>Clear</CButton>
                                    <CButton className='btn btn-secondary' data-bs-dismiss="modal" >Cancel</CButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    )
}

export default FileUploadComponent