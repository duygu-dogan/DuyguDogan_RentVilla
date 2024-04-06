import React, { useState } from 'react'
import { useEffect } from 'react'
import * as signalR from "@microsoft/signalr";
import { ToastContainer, toast } from 'react-toastify';

const SignalRService = ({ procedureName, setMessNumber }) => {
    const [receivedMessage, setReceivedMessage] = useState('');

    useEffect(() => {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:5006/product-hub")
            .withAutomaticReconnect()
            .build();
        connection.start().then(() => {
            console.log('Connected!');
            // setTimeout(() => { connection.start("http://localhost:5006/product-hub"), 2000 });
        }).catch((error) => {
            console.log('Error: ' + error);
        });
        connection.on(procedureName, (message) => {
            setReceivedMessage(message);
            console.log('Received message: ' + message)

            const unreadMessages = JSON.parse(localStorage.getItem('unreadMessages')) || 0;
            localStorage.setItem('unreadMessages', JSON.stringify(unreadMessages + 1));

            const messages = JSON.parse(localStorage.getItem('messages')) || [];
            messages.push(message);
            localStorage.setItem('messages', JSON.stringify(messages));

            toast(message, { type: 'info' })
        });
        connection.onreconnected((connectionId) => {
            console.log('Reconnected with connectionId: ' + connectionId);
        });
        connection.onreconnecting((error) => {
            console.log('Reconnecting');
        });
        // connection.invoke(procedureName, { user, message })
        //     .catch((error) => { console.log('Error: ' + error) });
        connection.onclose(() => {
            console.log('Connection closed');
        });
    }, []);
    return (
        <>
            <ToastContainer />
        </>
    )
}

export default SignalRService