"use client";

import React from "react";
import { useEffect } from "react";
import { UselessTask } from "../models/UselessTask";
import TaskView from "../_components/tasks-view";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
//npm install @microsoft/signalr
export default function Home() {

  const [tasks, setTasks] = React.useState<UselessTask[]>([]);
  const [hubConnection, setHubConnection] = React.useState<HubConnection>();
  useEffect(() => {
      connecttohub();
    }, []);

  function connecttohub() {
    let testTasks = new Array<UselessTask>(
          { id: 1, text: "Test Task 1", completed: false },
          { id: 2, text: "Test Task 2", completed: true });
     
    // TODO On doit commencer par créer la connexion vers le Hub
    let newHubConnection = new HubConnectionBuilder()
    .withUrl('http://localhost:5042/leHub/tasks')
    .build();
    // TODO On peut commencer à écouter pour les messages qui vont déclencher des callbacks (Il y a seulement un message pour commencer)
    newHubConnection.on('GetAllTasks', (data) => {
  setTasks(data);
});

newHubConnection
  .start()
  .then(() => {setHubConnection(newHubConnection);
    console.log("Connexion réussie !");})

    // TODO On doit ensuite se connecter
    newHubConnection!.invoke('AddTask', tasks);
  }

 async function onTaskToggle(id: number) {
    if (!hubConnection) return;

    await hubConnection.invoke("CompleteTask", id);
  }

  async function handleTaskAdd(taskName: string) {
    if (!hubConnection) return;

    await hubConnection.invoke("AddTask", taskName);
  }

  return (
    <div className="p-4">
        <h1>SignalR!</h1>
        <TaskView 
          tasks={tasks} 
          onTaskAdd={handleTaskAdd}
          onTaskToggle={onTaskToggle}
        />
    </div>
  );
}