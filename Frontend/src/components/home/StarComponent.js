import React from "react";
import { v4 } from "uuid";
import { useState } from 'react';

class StarComponent extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            count: props.Count,
            rating: props.Rating,
        };
        // this.Event = props.Event;
    }

    // event = (newRating) => { 
    //     this.Event(newRating);
    //     this.setState(() => ({
    //         rating: newRating
    //       }));
    // };
    
    getStar = () => {
        var children = [];
        for (var i = 0; i < this.state.count; i++) {
            let a = React.createElement('span',{
                className: i >= this.state.rating ? 'fa fa-star-o' : 'fa fa-star',
                key: v4(),
            }, null);
            children.push(React.createElement('a', { color: 'black', key: v4()}, a));
        }
        return children;
    }

    render() {
        return (
            <p className="ratings">
                {this.getStar()}
            </p>
        );
    }
}

export default StarComponent;