### Architecture
1. 1970s Dumb terminals -> Sever-based computing
2. 2026 Internet Browser as Dumb Terminal -> Blazor Server-based computing

### Transport
Opaque SignalR messages are sent between the client and server. 
The client sends user interactions to the server, and the server sends UI updates back to the client. 
This allows for real-time communication and a responsive user experience.

SignalR abstracts away the complexities of managing WebSocket connections, 
allowing developers to focus on building their applications without worrying 
about the underlying transport mechanism.

#### SignalR Transport Mechanisms
1. WebSockets: The preferred transport mechanism for real-time communication. It provides full-duplex
2. Server-Sent Events (SSE): A fallback option for browsers that do not support WebSockets. It allows the server to push updates to the client over a single HTTP connection.
3. Long Polling: Another fallback option for browsers that do not support WebSockets or SSE. It involves the client making a request to the server and waiting for a response. If the server has an update, it responds immediately; otherwise, it holds the request until an update is available or a timeout occurs.
4. Forever Frame: A legacy transport mechanism for older versions of Internet Explorer. It involves the server sending a continuous stream of HTML to the client, which is then parsed and executed by the browser.
5. SignalR automatically selects the best available transport mechanism based on the client's capabilities and network conditions, ensuring a seamless experience for users regardless of their browser or device.

#### Transport Problem
The problem is that this type of transport is opaque, meaning that the client and server do not have direct access to the underlying messages being sent. This can make it difficult to debug and optimize the communication between the client and server, as developers may not have visibility into the data being transmitted. Additionally, it can lead to performance issues if the messages are not properly optimized, as the client and server may be sending unnecessary data or experiencing latency due to the transport mechanism. To address this problem, developers may need to implement additional logging and monitoring tools to gain insights into the communication between the client and server, or consider alternative transport mechanisms that provide more visibility into the messages being sent.

### Blazor Server Mental Model - DOM Representation and Render Messages

1. On server app bootstrap eg. first time start, Blazor Server creates a complete DOM representation of the UI on the server.
2. That DOM is then serialized and sent to the client as a single message, which is used to render the initial UI on the client side. This is classic SSR (Server-Side Rendering) approach, where the server is responsible for generating the HTML and sending it to the client for rendering.
3. After the initial render, any user interactions on the client side (such as clicks, input changes, etc.) are sent back to the server as messages. The server processes these interactions, updates the DOM representation accordingly, and then sends back only the necessary changes to the client to update the UI. This allows for a more efficient communication between the client and server, as only the relevant updates are sent rather than re-rendering the entire UI. Render messages are in the form of a diff but binary encoded for efficiency. This approach allows for a responsive user experience while minimizing the amount of data sent between the client and server.
4. As RenderMessages are in binary format, they are more compact and efficient to transmit over the network compared to traditional text-based formats like JSON or XML. This can lead to faster communication between the client and server, as well as reduced bandwidth usage. Additionally, binary encoding can help to reduce latency and improve overall performance, especially in scenarios where there are frequent updates or large amounts of data being transmitted. However, it may require additional processing on both the client and server sides to encode and decode the messages, which could introduce some overhead. Overall, using binary encoding for RenderMessages can be a beneficial approach for optimizing communication in Blazor Server applications, but it is important to carefully consider the trade-offs and ensure that the benefits outweigh the potential drawbacks in terms of complexity and performance.
5. Another drawback of using binary encoding for RenderMessages is that it can make debugging and troubleshooting more difficult. Since the messages are not human-readable, it may be harder for developers to understand the content of the messages and identify any issues that may arise during development or in production. This can lead to increased development time and effort, as well as potential challenges in maintaining and updating the application over time. Additionally, if there are any issues with the encoding or decoding process, it could result in errors or unexpected behavior in the application, which may require additional debugging and troubleshooting efforts to resolve. Overall, while binary encoding can provide performance benefits, it is important to carefully consider the potential drawbacks and ensure that appropriate tools and processes are in place to effectively manage and troubleshoot any issues that may arise.

### Debugging Blazor Server Applications
Debugging Blazor Server applications can be challenging due to the nature of the communication between the client and server. Since the client and server are communicating through opaque SignalR messages, it can be difficult to understand what is happening behind the scenes and identify any issues that may arise. Additionally, since the RenderMessages are in binary format, it can be even more difficult to debug and troubleshoot issues related to the rendering of the UI.
To effectively debug Blazor Server applications, developers may need to implement additional logging and monitoring tools to gain insights into the communication between the client and server. This can include logging the messages being sent between the client and server, as well as any errors or exceptions that may occur during the processing of these messages. Additionally, developers may need to use specialized tools for debugging binary data, such as hex editors or binary viewers, to analyze the content of the RenderMessages and identify any issues related to the rendering of the UI.
Overall, debugging Blazor Server applications requires a combination of traditional debugging techniques, such as logging and monitoring, as well as specialized tools for analyzing binary data. It is important for developers to be aware of the unique challenges associated with debugging Blazor Server applications and to have the necessary tools and processes in place to effectively manage and troubleshoot any issues that may arise during development and in production.

#### Backward Nature of Blazor Debugging

1. Browser event (click)
2. SignalR message
3. DispatchEventAsync
4. Event handler runs
5. State changes
6. Render diff generated
7. Render batch sent to browser
8. Browser applies DOM patch
9. Browser sends OnRenderCompleted
10. OnAfterRender runs
11. Your logs appear

