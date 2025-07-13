# Project Structure Documentation

## Overview

The VR Table Tennis project follows a modular architecture designed for maintainability, extensibility, and clear separation of concerns. Each module has a specific responsibility and communicates with other modules through well-defined interfaces.

## Directory Structure

```
Assets/
├── Scripts/
│   ├── Core/                    # Core game management
│   │   └── GameManager.cs      # Central game controller
│   ├── Gameplay/               # Core gameplay mechanics
│   │   ├── Ball.cs            # Ball physics and collision
│   │   └── Tosser.cs          # Ball tossing system
│   ├── Interaction/            # VR interaction systems
│   │   ├── HandData.cs        # Hand tracking
│   │   └── PaddleAttacher.cs  # Paddle interaction
│   ├── Accessibility/          # BLV accessibility features
│   │   ├── AudioGuide.cs      # Audio guidance
│   │   └── HapticsController.cs # Haptic feedback
│   └── UI/                    # User interface
│       └── MenuController.cs   # Menu navigation
├── Scenes/
│   └── game scene.unity       # Main game scene
├── Prefabs/                   # Reusable game objects
├── Materials/                 # Visual materials
├── Models/                    # 3D models
├── Sounds/                    # Audio assets
└── Settings/                  # Project configuration
```

## Module Architecture

### 1. Core Module (`VRTableTennis.Core`)

**Purpose**: Central game management and coordination

**Components**:
- `GameManager`: Singleton pattern for game state management
- Handles game lifecycle (start, pause, restart, quit)
- Coordinates communication between modules
- Manages global game state

**Dependencies**: None (other modules depend on Core)

**Key Features**:
- Singleton pattern for global access
- Event-driven architecture
- Centralized game state management

### 2. Gameplay Module (`VRTableTennis.Gameplay`)

**Purpose**: Core gameplay mechanics and physics

**Components**:
- `Ball`: Ball physics, collision detection, audio feedback
- `Tosser`: Automatic ball tossing with configurable parameters

**Dependencies**: Core module

**Key Features**:
- Physics-based ball movement
- Collision detection and response
- Audio feedback for collisions
- Configurable ball tossing patterns

### 3. Interaction Module (`VRTableTennis.Interaction`)

**Purpose**: VR interaction and input handling

**Components**:
- `HandData`: Hand tracking and controller input
- `PaddleAttacher`: Paddle pickup and interaction mechanics

**Dependencies**: Core module

**Key Features**:
- VR controller input handling
- Hand tracking integration
- Paddle interaction mechanics
- Raycast-based object detection

### 4. Accessibility Module (`VRTableTennis.Accessibility`)

**Purpose**: BLV-specific accessibility features

**Components**:
- `AudioGuide`: Audio guidance for paddle location
- `HapticsController`: Haptic feedback system

**Dependencies**: Core module

**Key Features**:
- Distance-based audio guidance
- Haptic feedback patterns
- Spatial awareness systems
- Accessibility-focused design

### 5. UI Module (`VRTableTennis.UI`)

**Purpose**: User interface and menu systems

**Components**:
- `MenuController`: Menu navigation and scene management

**Dependencies**: Core module

**Key Features**:
- Menu navigation
- Scene transitions
- UI event handling
- Save/load functionality

## Communication Patterns

### 1. Singleton Pattern (GameManager)
```csharp
// Access from any module
GameManager.Instance.StartGame();
```

### 2. Event-Driven Communication
```csharp
// Subscribe to events
GameManager.Instance.OnGameStart += HandleGameStart;

// Publish events
GameManager.Instance.StartGame();
```

### 3. Component-Based Architecture
Each script is a Unity component that can be attached to GameObjects and configured through the Inspector.

## Data Flow

### Game Initialization
1. `GameManager` initializes and sets up core systems
2. Modules register with `GameManager` for event handling
3. VR systems initialize (hand tracking, controllers)
4. Accessibility systems start (audio, haptics)
5. Game becomes ready for player interaction

### Gameplay Loop
1. `Tosser` creates balls at regular intervals
2. `Ball` handles physics and collision detection
3. `AudioGuide` provides location feedback
4. `HapticsController` provides tactile feedback
5. `PaddleAttacher` handles player interaction
6. `GameManager` coordinates overall game state

### User Interaction
1. Player moves controllers (handled by `HandData`)
2. `PaddleAttacher` detects paddle pickup
3. `AudioGuide` stops location beeping
4. `Tosser` begins ball tossing
5. `Ball` provides collision feedback
6. `HapticsController` provides spatial feedback

## Configuration

### Inspector-Based Configuration
Most parameters are configurable through Unity's Inspector:
- Audio settings in `AudioGuide`
- Haptic settings in `HapticsController`
- Gameplay settings in `Tosser`
- Interaction settings in `PaddleAttacher`

### Scriptable Objects (Future Enhancement)
Consider using Scriptable Objects for:
- Game configuration profiles
- Accessibility settings
- Audio presets
- Haptic patterns

## Extension Points

### Adding New Gameplay Features
1. Create new script in appropriate module
2. Inherit from MonoBehaviour
3. Use `VRTableTennis.[ModuleName]` namespace
4. Register with `GameManager` if needed
5. Add XML documentation

### Adding New Accessibility Features
1. Create script in `Accessibility` module
2. Follow accessibility guidelines
3. Test with BLV users
4. Document accessibility benefits

### Adding New UI Features
1. Create script in `UI` module
2. Follow Unity UI patterns
3. Ensure accessibility compliance
4. Test with screen readers

## Testing Strategy

### Unit Testing
- Test individual components in isolation
- Mock dependencies for clean testing
- Focus on accessibility features

### Integration Testing
- Test module interactions
- Verify event communication
- Test VR interaction flows

### Accessibility Testing
- Test with BLV users
- Validate audio cues
- Test haptic feedback patterns
- Ensure screen reader compatibility

## Performance Considerations

### VR Performance
- Optimize for 90fps rendering
- Minimize draw calls
- Use object pooling for balls
- Optimize audio processing

### Accessibility Performance
- Ensure low-latency audio feedback
- Optimize haptic processing
- Minimize audio processing overhead
- Test with various hardware configurations

## Future Enhancements

### Planned Modules
- **Analytics**: Player behavior tracking
- **AI**: Adaptive difficulty system
- **Multiplayer**: Networked gameplay
- **Customization**: Player preferences

### Architecture Improvements
- Event system for loose coupling
- Dependency injection for testing
- Configuration management system
- Plugin architecture for accessibility features

## Best Practices

### Code Organization
- Keep scripts focused on single responsibilities
- Use meaningful namespaces
- Add comprehensive documentation
- Follow Unity conventions

### Accessibility
- Always consider BLV users in design
- Test with actual users
- Provide multiple feedback modalities
- Ensure graceful degradation

### Performance
- Profile regularly on target hardware
- Optimize for VR requirements
- Minimize garbage collection
- Use appropriate data structures 