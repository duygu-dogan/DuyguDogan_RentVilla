import React from 'react'

const ModalComponent = ({ buttonTitle, modalBody, onModalClick, buttonColor }) => {
    return (
        <div>
            <button type="button" class={`btn btn-${buttonColor}`} data-bs-toggle="modal" data-bs-target="#exampleModal">
                {buttonTitle}
            </button>

            <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            {modalBody}
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                            <button type="button" onClick={onModalClick} class="btn btn-primary" data-bs-dismiss="modal">Save changes</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default ModalComponent