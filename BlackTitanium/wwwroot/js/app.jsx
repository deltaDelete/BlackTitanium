import React from "react";

function Hello() {
    return (
        <h1>Привет, React.JS</h1>
    );
}

ReactDOM.render(
    <Hello />,
    document.getElementById("content")
);