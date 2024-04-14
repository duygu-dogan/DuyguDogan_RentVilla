import React, { useState } from 'react'
import { useEffect } from 'react'
import * as signalR from "@microsoft/signalr";
import { ToastContainer, toast } from 'react-toastify';

const SignalRService = ({ procedureNames, hubUrls }) => {
    const [receivedMessage, setReceivedMessage] = useState('');

    useEffect(() => {
        const connections = hubUrls.map((hubUrl, index) => {
            const connection = new signalR.HubConnectionBuilder()
                .withUrl(`http://localhost:5006/${hubUrl}`)
                .withAutomaticReconnect()
                .build();

            connection.start().then(() => {
                console.log(`Connected to ${hubUrl}!`);
            }).catch((error) => {
                console.log('Error: ' + error);
            });

            connection.on(procedureNames[index], (message) => {
                setReceivedMessage(message);
                console.log('Received message: ' + message)

                const unreadMessages = JSON.parse(localStorage.getItem('unreadMessages')) || 0;
                localStorage.setItem('unreadMessages', JSON.stringify(unreadMessages + 1));

                const messages = JSON.parse(localStorage.getItem('messages')) || [];
                messages.push(message);
                localStorage.setItem('messages', JSON.stringify(messages));

                toast(message, { type: 'info' })
            });
            return connection;
        });
        return () => {
            connections.forEach(connection => {
                connection.stop().then(() => console.log(`Disconnected from ${connection.connectionId}`));
            });
        }
    }, []);
    return (
        <>
            <ToastContainer />
        </>
    )
}

export default SignalRService